import { Menu, MenuProps } from "@components/Menu"
import { UserContext } from "@contexts"
import { USER_ACTION } from "@contexts/UserContext"
import { IUser } from "@interfaces"
import { AppRequest } from "@requests"
import { useContext, useEffect, useState } from "react"
import { Outlet, useNavigate } from "react-router"
import { Button } from '@components';
import backIcon from "/back-home.svg"
import { useLocation } from "react-router"
import { toast } from "react-toastify"
import { AxiosError } from "axios"
import { AppSocket } from "@websocket"

export const HomeLayout: React.FC = () => {
    const { dispatchUser } = useContext(UserContext)
    const navigate = useNavigate()
    const location = useLocation()
    const [data] = useState<MenuProps>({
        tabs: [
            {
                title: "Self Evaluation",
                items: [
                    {
                        index: 0,
                        title: "Evaluation",
                        navigateTo: "/home/self-evaluation"
                    },
                    {
                        index: 1,
                        title: "History",
                        navigateTo: "/home/self-evaluation/history"
                    },
                    {
                        index: 2,
                        title: "Result",
                        navigateTo: "/home/self-evaluation/result"
                    }
                ]
            }
        ]
    })

    useEffect(() => {
        AppSocket.getInstance().init(`wss://localhost:7061/connect?token=${localStorage.getItem("jwt")}`, dispatchUser)
    }, [])

    useEffect(() => {
        fetchUserInfo()
    }, [])

    const fetchUserInfo = async () => {
        try {
            const resp = await AppRequest.getInstance().get("api/Profile/me")

            if (resp.data?.message === "success") {
                dispatchUser({
                    type: USER_ACTION.ADD,
                    payload: {
                        ...resp.data?.data
                    } as IUser
                })
            }

        } catch (e) {
            const err = e as AxiosError
            toast.error(`Has error (${err?.code})`)
        }
    }

    const handleHome = () => {
        navigate("/home")
    }

    return <>
        <div className="min-h-screen min-w-[1024px] flex">
            <span className="flex bg-[#f5f5f5] w-full gap-5 px-5">
                <div className="flex pt-5 flex-col gap-2 basis-[20%] min-w-[240px] bg-[#f5f5f5]">
                    <Button onClick={handleHome} className="bg-red-400" disabled={location.pathname === "/home"}>
                        <img src={backIcon} className="w-[25px] h-[25px]" />
                    </Button>
                    <Menu data={data} />
                </div>
                <div className="py-5 basis-[80%] min-w-[640px] min-h-[100%]">
                    <Outlet />
                </div>
            </span>
        </div>
    </>
}