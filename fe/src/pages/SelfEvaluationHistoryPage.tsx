import React, { useContext, useEffect, useState } from "react";
import { AppRequest } from '@requests';
import { IAchievementItem, ICriteria, IEvaluationSchedule, IEvaluationScore, IPerformanceEvaluation } from "@interfaces";
import { UserContext } from '@contexts';
import { AxiosError } from "axios";
import { toast } from "react-toastify";
import moment from "moment";

export const SelfEvaluationHistoryPage: React.FC = () => {
    const { user } = useContext(UserContext)
    const [scores, setScores] = useState<IEvaluationScore[]>([])
    const [evaluationSchedules, setevaluationSchedules] = useState<IEvaluationSchedule[]>([])

    useEffect(() => {
        fetchSelfHistory()
    }, [])

    const fetchSelfHistory = async () => {
        const { data } = await AppRequest.getInstance().get(`api/EvaluationScore/Self`)

        let selfScoresByCriterias: IEvaluationScore[] = data?.data
        selfScoresByCriterias = selfScoresByCriterias.filter(v => v.sourceId === user.id && v.targetId === user.id)
        setScores([...selfScoresByCriterias])
    }

    return <div className="flex flex-col h-[500px] bg-[#efefef]">
            <div className="overflow-y-scroll p-5 h-[100%]">
                <ul className="block space-y-2 h-[100%]">
                    {
                        scores?.length > 0 && scores.map((v, i) => <li key={i}>[{moment((v.createdAt ?? 0) * 1000).format("DD/MM/yyyy hh:mm:ss A")}] Scored <b>{v.score}</b> ({v.criteria?.content})</li>) || (<b>No history now ✔</b>)
                    }
                </ul>
            </div>
        </div>
}