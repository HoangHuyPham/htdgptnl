export interface IUser{
    id? : string
    username? : string
    email? : string
    phone? : string
    roleId? : string
    employeeId? : string
}

export interface IRole{
    id?: string,
    name?: string,
    description?: string
}

export interface ISchedule{
    id?: string,
    start?: Date,
    end?: Date,
    description?: string
}

export interface IRoleType{
    id?: string,
    name?: string
}

export interface ICriteria{
    id?: string,
    content?: string,
    evidenceRequired?: boolean,
    achievementItemId?: string
}

export interface IAchievementItem{
    id?: string,
    name?: string,
    threshold?: number,
    target?: number,
    stretch?: number,
    weight?: number,
    criterias?: ICriteria[]
}

export interface IAchievement{
    id?: string,
    name?: string,
    threshold?: number,
    target?: number,
    stretch?: number,
    weight?: number,
    achievementItems?: IAchievementItem[]
}

export interface IPerformanceEvaluation{
    id?: string,
    name?: string,
    start?: number,
    end?: number,
    createdAt?: number,
    achievements?: IAchievement[]
}

export interface IEvaluationSchedule{
    id?: string,
    isSelfEvalution?: boolean,
    description?: string,
    start?: number,
    end?: number,
    performanceEvaluation?: IPerformanceEvaluation,
}

export interface IEvidence{
    id?: string
}

export interface IEvaluationScore{
    id?: string,
    score?: number,
    comment?: string,
    createdAt?: number,
    sourceId?: string,
    targetId?: string,
    criteriaId?: string,
    criteria?: ICriteria
    evidences?: IEvidence[]
}
