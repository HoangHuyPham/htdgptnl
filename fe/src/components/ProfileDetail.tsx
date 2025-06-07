import { IUser } from "@interfaces"
import { Button } from "./Button"
import { useCallback, useContext, useState } from "react"
import { AppRequest } from "@requests"
import { USER_ACTION, UserContext } from "@contexts/UserContext"
import { AxiosError, HttpStatusCode } from "axios"
import { toast } from "react-toastify"
import { useNavigate } from "react-router"


const ModalPhoneNumber: React.FC<{ isHide: boolean, handleChangePhone: () => void }> = ({ isHide, handleChangePhone }) => {
    const [phone, setPhone] = useState()
    const [isLoading, setLoading] = useState(false)
    const { dispatchUser } = useContext(UserContext)

    const fetchUserInfo = async () => {
        try {
            const resp = await AppRequest.getInstance().get("api/Profile/me")

            if (resp.data?.message === "success") {
                dispatchUser({
                    type: USER_ACTION.UPDATE,
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

    const handleSave = async () => {
        setLoading(true)
        try {
            const resp = await AppRequest.getInstance().post("api/Profile/ChangePhone", {
                newPhone: phone,
            })

            if (resp.status === HttpStatusCode.Ok) {
                await fetchUserInfo()
                toast.success("Change phone success")
            } else {
                toast.error("Change phone failed")
            }
        } catch (e) {
            const err = e as AxiosError
            if (err.status === HttpStatusCode.Forbidden)
                toast.error("Change phone failed")
        } finally {
            setLoading(false)
        }
    }

    return <div hidden={isHide} className="absolute transition-all flex w-full h-screen justify-center items-center top-0 left-0 bg-[rgb(1,1,1,0.5)]">
        <div className="fixed animate-fadeIn shadow-2xl gap-2 flex flex-col rounded-2xl w-[240px] p-5 bg-simple-white">
            <span className="text-xl font-semibold">Change Phone</span>
            <input className="border-b-1 px-1" type="number" value={phone} onChange={(e) => setPhone(e?.target?.value)} placeholder="phone" />
            <Button disabled={isLoading} isLoading={isLoading} onClick={handleSave}>Save</Button>
            <Button onClick={handleChangePhone} className="bg-red-500">Back</Button>
        </div>
    </div>
}

const ModalEmail: React.FC<{ isHide: boolean, handleChangeEmail: () => void }> = ({ isHide, handleChangeEmail }) => {
    const [email, setEmail] = useState()
    const [isLoading, setLoading] = useState(false)
    const { user } = useContext(UserContext)

    const handleSave = async () => {
        setLoading(true)
        try {
            const resp = await AppRequest.getInstance().post("api/Profile/ChangeEmail", {
                newEmail: email
            })

            if (resp.status === HttpStatusCode.Ok) {
                toast.success("Change email success")
            } else if (resp.status === HttpStatusCode.NoContent) {
                toast.info(`Sent a validation link to ${user.email}`)
            } else {
                toast.error("Change email failed")
            }
        } catch (e) {
            const err = e as AxiosError
            toast.error(`Has error (${err?.code})`)
        } finally {
            setLoading(false)
        }
    }

    return <div hidden={isHide} className="absolute transition-all flex w-full h-screen justify-center items-center top-0 left-0 bg-[rgb(1,1,1,0.5)]">
        <div className="fixed animate-fadeIn shadow-2xl gap-2 flex flex-col rounded-2xl w-[240px] p-5 bg-simple-white">
            <span className="text-xl font-semibold">Change Email</span>
            <input className="border-b-1 px-1" type="text" value={email} onChange={(e) => setEmail(e?.target?.value)} placeholder="email" />
            <Button disabled={isLoading} isLoading={isLoading} onClick={handleSave}>Save</Button>
            <Button onClick={handleChangeEmail} className="bg-red-500">Back</Button>
        </div>
    </div>
}

const ModalPassword: React.FC<{ isHide: boolean, handleChangePassword: () => void }> = ({ isHide, handleChangePassword }) => {
    const [password, setPassword] = useState()
    const [isLoading, setLoading] = useState(false)
    const [newPassword, setNewPassword] = useState()
    const [confirmPassword, setConfirmPassword] = useState()
    const { user, dispatchUser } = useContext(UserContext)
    const navigate = useNavigate()

    const handleSave = async () => {
        setLoading(true)
        try {
            const resp = await AppRequest.getInstance().post("api/Auth/ChangePassword", {
                username: user.userName,
                oldPassword: password,
                password: newPassword
            })

            if (resp.status === HttpStatusCode.Ok) {
                toast.success("Change password success")
                dispatchUser({
                    type: USER_ACTION.CLEAR,
                    payload: null
                })
                localStorage.removeItem("jwt")
                setTimeout(() => {
                    navigate("/login")
                }, 1000);
            } else {
                toast.error("Change password failed")
            }
        } catch (e) {
            const err = e as AxiosError
            if (err.status === HttpStatusCode.Forbidden)
                toast.error("Change password failed")
        } finally {
            setLoading(false)
        }
    }

    return <div hidden={isHide} className="absolute transition-all flex w-full h-screen justify-center items-center top-0 left-0 bg-[rgb(1,1,1,0.5)]">
        <div className="fixed animate-fadeIn shadow-2xl gap-2 flex flex-col rounded-2xl w-[240px] p-5 bg-simple-white">
            <span className="text-xl font-semibold">Change Password</span>
            <input className="border-b-1 px-1" type="password" value={password} onChange={(e) => setPassword(e?.target?.value)} placeholder="password" />
            <input className="border-b-1 px-1" type="password" value={newPassword} onChange={(e) => setNewPassword(e?.target?.value)} placeholder="new password" />
            <input className="border-b-1 px-1" type="password" value={confirmPassword} onChange={(e) => setConfirmPassword(e?.target?.value)} placeholder="confirm password" />
            <Button disabled={isLoading} isLoading={isLoading} onClick={handleSave}>Save</Button>
            <Button onClick={handleChangePassword} className="bg-red-500">Back</Button>
        </div>
    </div>
}


export const ProfileDetail: React.FC<{ user: IUser }> = ({ user }) => {
    const [isPhoneModalHide, setPhoneModalHide] = useState(true)
    const [isEmailModalHide, setEmailModalHide] = useState(true)
    const [isPasswordModalHide, setPasswordModalHide] = useState(true)

    const handleChangePhone = useCallback(() => {
        setPhoneModalHide(prev => !prev)
    }, [])
    const handleChangeEmail = useCallback(() => {
        setEmailModalHide(prev => !prev)
    }, [])
    const handleChangePassword = useCallback(() => {
        setPasswordModalHide(prev => !prev)
    }, [])

    return <>
        <ModalPhoneNumber isHide={isPhoneModalHide} handleChangePhone={handleChangePhone} />
        <ModalEmail isHide={isEmailModalHide} handleChangeEmail={handleChangeEmail} />
        <ModalPassword isHide={isPasswordModalHide} handleChangePassword={handleChangePassword} />

        <span>
            <div className="bg-white px-5 text-4xl font-semibold">Profile</div>
            <div className="flex justify-center gap-5 p-5 bg-white">
                <img className="w-[240px] h-[240px] rounded-[50%] border-simple-black border-1" src="https://i.pinimg.com/474x/8d/49/02/8d49020a8f5ae482d8f89fd6c7b7e399.jpg" />

                <div className="flex flex-col justify-center">
                    <span className="flex text-2xl w-[420px] justify-between">
                        <span>Fullname: </span>
                        <input disabled className="w-[280px] font-semibold" type="text" value={user?.employee?.detail?.fullName} />
                    </span>

                    <span className="flex text-2xl w-[420px] justify-between">
                        <span>Username: </span>
                        <input disabled className="w-[280px] font-semibold" type="text" value={user?.userName} />
                    </span>

                    <span className="flex text-2xl w-[420px] justify-between">
                        <span>Email: </span>
                        <input disabled className="w-[280px] font-semibold" type="text" value={user?.email} />
                    </span>

                    <span className="flex text-2xl w-[420px] justify-between">
                        <span>Phone: </span>
                        <input disabled className="w-[280px] font-semibold" type="text" value={user?.phone} />
                    </span>

                    <span className="flex text-2xl w-[420px] justify-between">
                        <span>Role: </span>
                        <input disabled className="w-[280px] font-semibold" type="text" value={user?.role?.name} />
                    </span>

                    <span className="flex p-2 font-bold w-[420px] gap-2 justify-center">
                        <Button onClick={handleChangePassword}>Change password</Button>
                        <Button onClick={handleChangeEmail} className="bg-orange-400">Change email</Button>
                        <Button onClick={handleChangePhone} className="bg-green-500">Change phone</Button>
                    </span>
                </div>
            </div>
        </span>
    </>
}