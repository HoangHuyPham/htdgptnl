import formTableStyle from "@styles/form-table.module.css"
import moment from "moment";
import { IAchievement, IAchievementItem, ICriteria, IEvaluationSchedule, IEvaluationScore } from "@interfaces"
import { ChangeEvent, memo, useContext, useEffect, useState } from "react"
import { Button } from '@components';
import { toast } from "react-toastify";
import { AppRequest } from "@requests";
import { ScoreTempContext, ScoreTempProvider, ScoreContext, ScoreProvider, UserContext } from "@contexts";
import { SCORE_ACTION } from "@contexts/ScoreContext";
import { SCORE_TEMP_ACTION } from "@contexts/ScoreTempContext";
import { AxiosError } from "axios";

export const FormTable: React.FC<{ evaluationSchedule?: IEvaluationSchedule }> = ({ evaluationSchedule }) => {
    return <>
        <div className="shadow-2xl px-5">
            <div className="flex flex-col p-5 justify-between bg-purple-600 text-simple-white text-2xl">
                <span className="font-semibold">Form: {evaluationSchedule?.performanceEvaluation?.name}</span>
                <span className="text-xl"><span className="font-semibold">{`${moment((evaluationSchedule?.performanceEvaluation?.start ?? 0) * 1000).format("DD/MM/yyyy hh:mm:ss A")} - ${moment((evaluationSchedule?.performanceEvaluation?.end ?? 0) * 1000).format("DD/MM/yyyy hh:mm:ss A")}`}</span></span>
                <span className="text-xl">Duration (you): <span className={`font-semibold ${(moment().unix() < (evaluationSchedule?.start ?? 0) || moment().unix() > (evaluationSchedule?.end ?? 0)) ? "text-red-300": "text-green-400"}`}>{`${moment((evaluationSchedule?.start ?? 0) * 1000).format("DD/MM/yyyy hh:mm:ss A")} - ${moment((evaluationSchedule?.end ?? 0) * 1000).format("DD/MM/yyyy hh:mm:ss A")}`}</span></span>
                <span className="text-xl">Description: {evaluationSchedule?.description}</span>
            </div>
            {
                evaluationSchedule?.performanceEvaluation?.achievements?.map((v, i) => <ScoreProvider key={i}><ScoreTempProvider><Achievement achievement={v} /></ScoreTempProvider></ScoreProvider>)
            }
        </div>
    </>
}

const Achievement: React.FC<{ achievement?: IAchievement }> = ({ achievement }) => {
    const [isLoading, setLoading] = useState(false)
    const [isHide, setHide] = useState(false)
    const { dispatchScore } = useContext(ScoreContext)
    const { tempScores } = useContext(ScoreTempContext)
    const { user } = useContext(UserContext)

    useEffect(() => {
        fetchSelfScores()
    }, [])


    const fetchSelfScores = async () => {
        const fiteredScores: IEvaluationScore[] = []

        for (const achievementItem of achievement?.achievementItems ?? []) {
            for (const criteria of achievementItem?.criterias ?? []) {
                const { data } = await AppRequest.getInstance().get(`api/EvaluationScore/Self`, {
                    params: {
                        criteriaId: criteria.id
                    }
                })

                let selfScoresByCriteria: IEvaluationScore[] = data?.data
                selfScoresByCriteria = selfScoresByCriteria.filter(v => v.sourceId === user.id && v.targetId === user.id)

                const lastestScoreByCriteria: IEvaluationScore | null = selfScoresByCriteria.reduce<IEvaluationScore | null>((prev, current) => {
                    if ((prev?.createdAt ?? 0) < (current.createdAt ?? 0)) {
                        return current
                    } else
                        return prev
                }, null)

                if (lastestScoreByCriteria)
                    fiteredScores.push(lastestScoreByCriteria)
            }
        }

        dispatchScore({
            type: SCORE_ACTION.ADD_OR_MERGE,
            payload: fiteredScores
        })
    }

    const handleSave = async () => {
        if (tempScores.length <= 0) {
            toast.info("No changes to save.")
            return
        }

        for (const tempScore of tempScores) {
            const achievementItem = achievement?.achievementItems?.find(x => x.criterias?.find(x => x.id === tempScore.criteriaId))
            const threshold = achievementItem?.threshold
            const stretch = achievementItem?.stretch
            const score = tempScore.score
            const criteria = achievementItem?.criterias?.find(x => x.id === tempScore.criteriaId)

            if (!threshold || !stretch || !score || !criteria) {
                toast.error("Has invalid param.")
                return
            }

            if (score < threshold || stretch < score) {
                toast.error(`Score ${score} for criteria "${criteria.content}" is out-ranged! (ignored)`)
                continue
            }

            try {
                await toast.promise(AppRequest.getInstance().post("api/EvaluationScore", {
                    score,
                    targetId: tempScore.targetId,
                    criteriaId: tempScore.criteriaId
                }), {
                    pending: `Set score [${score}] for criteria "${criteria.content}"...`,
                    error: `Set score [${score}] for criteria "${criteria.content}" failed`,
                    success: `Set score [${score}] for criteria "${criteria.content}" successfully`
                })
            } catch (e) {
                console.error(e)
            }
        }





    }
    return <>
        <div className="flex justify-between bg-blue-400 text-simple-white text-3xl">
            <span>{achievement?.name}</span>
            <span onClick={() => setHide(prev => !prev)}>{isHide && "➕" || "➖"}</span>
        </div>
        <div hidden={isHide}>
            <table className={formTableStyle.base}>
                <thead>
                    <tr>
                        <th>Achievement</th>
                        <th>Min</th>
                        <th>Max</th>
                        <th>Self</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        achievement?.achievementItems && achievement.achievementItems?.map((v, i) => <AchievementItem key={i} name={v.name} stretch={v.stretch} threshold={v.threshold} criterias={v.criterias} />)
                    }
                </tbody>
            </table>
            <span className="flex justify-center p-2">
                <Button disabled={isLoading || tempScores.length <= 0} isLoading={isLoading} onClick={handleSave}>Save</Button>
            </span>
        </div>
    </>
}

const AchievementItem: React.FC<IAchievementItem & { threshold?: number, stretch?: number }> = memo(({ threshold, stretch, name, criterias }) => {
    const [isHide, setHide] = useState(false)

    return <>
        <tr className={formTableStyle.achievement}>
            <td className="cursor-pointer text-simple-white" onClick={() => setHide(!isHide)}>
                <span>{name}</span>
                <span>{isHide && "➕" || "➖"}</span>
            </td>
        </tr>

        {criterias && criterias.map((v, i) => <CriteriaRow {...v} threshold={threshold || 0} stretch={stretch || 0} isHide={isHide} key={i} />)}
    </>
})

const CriteriaRow: React.FC<ICriteria & { isHide?: boolean, threshold: number, stretch: number }> = (criteria) => {
    const [score, setScore] = useState<IEvaluationScore | undefined>()
    const [isNewScore, setNewScore] = useState(false)
    const { scores } = useContext(ScoreContext)
    const { tempScores, dispatchTempScore } = useContext(ScoreTempContext)
    const { user } = useContext(UserContext)

    useEffect(() => {
        const matched = scores.find(v => v.criteriaId === criteria.id)
        if (matched)
            setScore({ ...matched })
        else
            setScore(undefined)
    }, [scores, criteria.id])

    useEffect(() => {
        const hasNewScore = tempScores.some(score => score.criteriaId === criteria.id);
        setNewScore(hasNewScore);
    }, [tempScores, criteria.id]);

    const handleScoreChange = (e: ChangeEvent<HTMLInputElement>) => {
        const score = e?.target?.value
        const parseScore = Number.parseInt(score)
        let shouldCreateTemp = true

        setScore(prev => {
            if (prev) {
                return { ...prev, score: parseScore }
            } else {
                return {
                    criteriaId: criteria.id,
                    sourceId: user.id,
                    targetId: user.id,
                    score: parseScore
                } as IEvaluationScore
            }
        })

        scores.forEach(v => {
            if ((v.criteriaId === criteria.id) && (v.sourceId === user.id) && (v.score === parseScore)) {
                shouldCreateTemp = false
                dispatchTempScore({
                    type: SCORE_TEMP_ACTION.REMOVE,
                    payload: v
                })
            }
        })
        if (shouldCreateTemp) {
            if (Number.isNaN(parseScore)) {
                dispatchTempScore({
                    type: SCORE_TEMP_ACTION.REMOVE,
                    payload: {
                        criteriaId: criteria.id,
                        sourceId: user.id,
                        targetId: user.id
                    } as IEvaluationScore
                })
                return
            }
            dispatchTempScore({
                type: SCORE_TEMP_ACTION.ADD_OR_MERGE,
                payload: {
                    score: parseScore,
                    criteriaId: criteria.id,
                    targetId: user.id,
                    sourceId: user.id
                } as IEvaluationScore
            })
        }
    }

    return <>

        {
            !criteria.isHide && <tr className={`${formTableStyle.criteria} animate-fadeIn`}>
                <td><span className="w-full">{criteria.content}</span></td>
                <td><span className="w-full text-center">{criteria.threshold}</span></td>
                <td><span className="w-full text-center">{criteria.stretch}</span></td>
                <td>
                    <input className={`${isNewScore ? "text-blue-500" : ""} ${score?.score && ((score?.score < criteria.threshold) || (score?.score > criteria.stretch)) ? "text-red-500 font-semibold" : ""}`} type="number" value={score?.score ?? ""} onChange={handleScoreChange} />
                </td>
            </tr>
        }
    </>
}