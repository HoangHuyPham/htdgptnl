import userIcon from "/user.svg"
import logoutIcon from "/logout.svg"
import { useContext } from "react"
import { ScoreContext, ScoreTempContext, UserContext } from '@contexts';
import { USER_ACTION } from "@contexts/UserContext";
import { useNavigate } from "react-router";
import { SCORE_ACTION } from "@contexts/ScoreContext";
import { SCORE_TEMP_ACTION } from "@contexts/ScoreTempContext";

export const AccountPage : React.FC = ()=>{
    const { dispatchUser } = useContext(UserContext)

    const navigate = useNavigate()

    const handleLogout = ()=>{
        dispatchUser({
            type: USER_ACTION.CLEAR,
            payload: null
        })
        localStorage.removeItem("jwt")
        setTimeout(() => {
            navigate("/login")
        }, 1000);
    }

    return <>
        <div className="flex gap-2 justify-around px-5 flex-wrap">
            <span className="basis-[100%] shrink-1 transition-all animate-fadeIn shadow-xl text-white cursor-pointer hover:bg-orange-300 flex flex-col items-center justify-center rounded-xl w-[230px] h-[230px] bg-blue-400">
                <img className="w-[120px] h-[120px]" src={userIcon}/>
                <span className="text-3xl font-semibold">Profile</span>
            </span>
            <span onClick={handleLogout} className="basis-[100%] shrink-1 transition-all animate-fadeIn shadow-xl text-white cursor-pointer hover:bg-orange-300 flex flex-col items-center justify-center rounded-xl w-[230px] h-[230px] bg-red-400">
                <img className="w-[120px] h-[120px]" src={logoutIcon}/>
                <span className="text-3xl font-semibold">Logout</span>
            </span>
        </div>
    </>
}