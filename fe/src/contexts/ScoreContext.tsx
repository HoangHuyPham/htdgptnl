import { IEvaluationScore } from "@interfaces";
import { createContext, Dispatch, ReactNode, Reducer, useReducer } from "react";

interface ScoreType {
    scores: IEvaluationScore[]
    dispatchScore: Dispatch<ScoreAction>
}

const SCORE_ACTION = {
    ADD_OR_MERGE: "ADD_OR_MERGE",
    CLEAR: "CLEAR",
}

interface ScoreAction {
    type: string,
    payload: IEvaluationScore[] | IEvaluationScore | null
}

const ScoreReducer: Reducer<IEvaluationScore[], ScoreAction> = (state, action) => {
    switch (action.type) {
        case SCORE_ACTION.ADD_OR_MERGE:
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
        case SCORE_ACTION.CLEAR:
            return []
        default:
            return state
    }
}


const ScoreContext = createContext<ScoreType>({} as ScoreType)

const ScoreProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [scores, dispatchScore] = useReducer(ScoreReducer, [])
    return <ScoreContext.Provider value={{ scores, dispatchScore }}>
        {children}
    </ScoreContext.Provider>
}

export {
    ScoreProvider, ScoreContext,
    // eslint-disable-next-line react-refresh/only-export-components
    SCORE_ACTION
}
