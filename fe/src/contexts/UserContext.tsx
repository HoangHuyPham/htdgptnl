import { IUser } from "@interfaces";
import { createContext, Dispatch, ReactNode, Reducer, useReducer } from "react";

interface UserType {
    user: IUser,
    dispatch: Dispatch<UserAction>
}

interface UserAction {
    type: string,
    payload: IUser | null
}

const USER_ACTION = {
    ADD: "ADD",
    REMOVE: "REMOVE",
    UPDATE: "UPDATE"
}

const UserContext = createContext<UserType>({} as UserType)

const UserReducer: Reducer<IUser | null, UserAction> = (state, action) => {
    switch (action.type) {
        case USER_ACTION.ADD:
            return { ...action.payload } as IUser
        case USER_ACTION.REMOVE:
            localStorage.removeItem("jwt")
            return null
        case USER_ACTION.UPDATE:
            return { ...state, ...action.payload }
        default:
            return state
    }
}

const UserProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [user, dispatch] = useReducer(UserReducer, null)
    return <UserContext.Provider value={{ user, dispatch } as UserType}>
        {children}
    </UserContext.Provider>
}


export { 
    UserContext, UserProvider, 
    // eslint-disable-next-line react-refresh/only-export-components
    USER_ACTION
}
