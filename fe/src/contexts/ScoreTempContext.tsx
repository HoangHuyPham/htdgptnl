import { IEvaluationScore } from "@interfaces";
import { createContext, Dispatch, ReactNode, Reducer, useReducer } from "react";

interface ScoreType {
    tempScores: IEvaluationScore[]
    dispatchTempScore: Dispatch<ScoreAction>
}

const SCORE_TEMP_ACTION = {
    ADD_OR_MERGE: "ADD_OR_MERGE",
    REMOVE: "REMOVE",
    CLEAR: "CLEAR",
}

interface ScoreAction {
    type: string,
    payload: IEvaluationScore[] | IEvaluationScore | null
}

const ScoreTempReducer: Reducer<IEvaluationScore[], ScoreAction> = (state, action) => {
    switch (action.type) {
        case SCORE_TEMP_ACTION.ADD_OR_MERGE:
            if (Array.isArray(action.payload)) {
                const newState: IEvaluationScore[] = [...state]
                const scores: IEvaluationScore[] = action.payload

                scores.forEach(score => {
                    const index = newState.findIndex(v => (v.criteriaId === score.criteriaId) && (v.sourceId === score.sourceId) && (v.targetId === score.targetId))
                    if (index !== -1) {
                        newState[index] = { ...newState[index], ...score }
                    } else {
                        newState.push(score)
                    }
                })
                return newState
            } else {
                const newState: IEvaluationScore[] = [...state]
                const score: IEvaluationScore = action.payload as IEvaluationScore
                const index = newState.findIndex(v => (v.criteriaId === score.criteriaId) && (v.sourceId === score.sourceId) && (v.targetId === score.targetId))

                if (index !== -1) {
                    newState[index] = { ...newState[index], ...score }
                } else {
                    newState.push(score)
                }
                return newState
            }
            break
        case SCORE_TEMP_ACTION.REMOVE:
            if (Array.isArray(action.payload)) {
                const scores: IEvaluationScore[] = action.payload
                return state.filter(v => {
                    scores.forEach(score => {
                        if ((v.criteriaId === score.criteriaId) && (v.sourceId === score.sourceId) && (v.targetId === score.targetId)) {
                            return false
                        }
                    })
                    return true
                })
            } else {
                const score: IEvaluationScore | null = action.payload
                return state.filter(v => !((v.criteriaId === score?.criteriaId) && (v.sourceId === score?.sourceId) && (v.targetId === score?.targetId)))
            }
            break
        case SCORE_TEMP_ACTION.CLEAR:
            return []
        default:
            return state
    }
}


const ScoreTempContext = createContext<ScoreType>({} as ScoreType)

const ScoreTempProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [tempScores, dispatchTempScore] = useReducer(ScoreTempReducer, [])
    return <ScoreTempContext.Provider value={{ tempScores, dispatchTempScore }}>
        {children}
    </ScoreTempContext.Provider>
}

export {
    ScoreTempProvider, ScoreTempContext,
    // eslint-disable-next-line react-refresh/only-export-components
    SCORE_TEMP_ACTION
}
