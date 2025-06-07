export interface IUser {
    id?: string
    userName?: string
    email?: string
    phone?: string
    roleId?: string,
    role?: IRole,
    employeeId?: string,
    employee?: IEmployee
}

export interface ISocketMessage{
    type: string,
    content: string,
}

export interface IEmployee {
    id?: string,
    detail?: IEmployeeDetail
    supervisor?: IEmployee,
    employees?: IEmployee[]
}

export interface IPositionE {
    id?: string,
    name?: string
}

export interface IPlant {
    id?: string,
    name?: string
}

export interface IGrade {
    id?: string,
    name?: string
}

export interface IDepartment {
    id?: string,
    name?: string
}

export interface IProcess {
    id?: string,
    name?: string
}

export interface IOperation {
    id?: string,
    name?: string
}

export interface IGroup {
    id?: string,
    name?: string
}

export interface IEmployeeDetail {
    id?: string,
    code?: string,
    type?: string,
    fullName?: string,
    startDate?: number,
    eligible?: boolean,
    workingDetail?: null,
    employeeId?: string,
    gradeId?: string,
    grade?: IGrade,
    positionEId?: string,
    positionE?: IPositionE,
    plantId?: string,
    plant?: IPlant,
    departmentId?: string,
    department?: IDepartment,
    processId?: string,
    process?: IProcess,
    operationId?: string,
    operation?: IOperation,
    groupId?: string,
    group?: IGroup
}

export interface IRole {
    id?: string,
    name?: string,
    description?: string
}

export interface ISchedule {
    id?: string,
    start?: Date,
    end?: Date,
    description?: string
}

export interface IRoleType {
    id?: string,
    name?: string
}

export interface ICriteria {
    id?: string,
    content?: string,
    evidenceRequired?: boolean,
    achievementItemId?: string
}

export interface IAchievementItem {
    id?: string,
    name?: string,
    threshold?: number,
    target?: number,
    stretch?: number,
    weight?: number,
    criterias?: ICriteria[]
}

export interface IAchievement {
    id?: string,
    name?: string,
    threshold?: number,
    target?: number,
    stretch?: number,
    weight?: number,
    achievementItems?: IAchievementItem[]
}

export interface IPerformanceEvaluation {
    id?: string,
    name?: string,
    start?: number,
    end?: number,
    createdAt?: number,
    achievements?: IAchievement[]
}

export interface IEvaluationSchedule {
    id?: string,
    isSelfEvalution?: boolean,
    description?: string,
    start?: number,
    end?: number,
    performanceEvaluation?: IPerformanceEvaluation,
}

export interface IEvidence {
    id?: string
}

export interface IEvaluationScore {
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
