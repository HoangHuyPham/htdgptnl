import { IUser } from "@interfaces";
import { createContext, Dispatch, ReactNode, Reducer, useReducer } from "react";

interface UserType {
    user: IUser,
    dispatchUser: Dispatch<UserAction>
}

export interface UserAction {
    type: string,
    payload: IUser | null
}

const USER_ACTION = {
    ADD: "ADD",
    CLEAR: "CLEAR",
    UPDATE: "UPDATE"
}

const UserContext = createContext<UserType>({} as UserType)

const UserReducer: Reducer<IUser | null, UserAction> = (state, action) => {
    switch (action.type) {
        case USER_ACTION.ADD:
            return { ...action.payload } as IUser
        case USER_ACTION.CLEAR:
            localStorage.removeItem("jwt")
            return null
        case USER_ACTION.UPDATE:
            return { ...state, ...action.payload }
        default:
            return state
    }
}

const UserProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, dispatchUser] = useReducer(UserReducer, null)
    return <UserContext.Provider value={{ user, dispatchUser } as UserType}>
        {children}
    </UserContext.Provider>
}


export { 
    UserContext, UserProvider, 
    // eslint-disable-next-line react-refresh/only-export-components
    USER_ACTION
}
