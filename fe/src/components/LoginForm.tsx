import { Button } from "@components"
import { AppRequest } from "@requests"
import { AxiosError } from "axios"
import { MouseEvent, useEffect, useRef, useState } from "react"
import { useNavigate } from "react-router"
import { toast } from "react-toastify"

export const LoginForm: React.FC = () => {
    const [isLoading, setLoading] = useState(false)
    const [username, setUsername] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const [errors, setErrors] = useState<string[]>([])
    const isFirstInput = useRef(true)
    const navigate = useNavigate()

    useEffect(() => {
        if (!isFirstInput.current)
            handleError()
    }, [username, password, isFirstInput.current])

    const handleError = async () => {
        const errs = []
        if (username.trim() == "")
            errs.push("username can not be blank")
        if (password.trim() == "")
            errs.push("password can not be blank")
        setErrors(errs)
    }

    const handleLogin = async (e: MouseEvent) => {
        e.preventDefault()
        setLoading(true)
        try {
            const resp = await AppRequest.getInstance().post("api/Auth/login", { username, password })
            if (resp.data?.message === "login success") {
                localStorage.setItem("jwt", resp.data?.data)
                toast.success(resp.data?.message)
                setTimeout(() => {
                    navigate("/home")
                }, 1000);
            } else
                toast.error(resp.data?.message)
        } catch (e) {
            const err = e as AxiosError
            toast.error(`Has error (${err?.code})`)
        } finally {
            setLoading(false)
        }
    }

    return <form className="flex box-border flex-col animate-fadeIn bg-white text-simple-black p-10 gap-4 rounded-xl">
        <label className="text-center text-2xl font-semibold">Login to account</label>
        <span className="flex flex-col gap-5 font-semibold">
            <input className="bg-amber-200 w-[240px]" type="text" value={username} onChange={(e) => { setUsername(e.target.value); isFirstInput.current = false; }} placeholder="username / email" />
            <input className="bg-amber-200 w-[240px]" type="password" value={password} onChange={(e) => { setPassword(e.target.value); isFirstInput.current = false; }} placeholder="password" />
        </span>
        <span className="text-red-400 font-semibold">
            {
                errors.map((v, i) => <p key={i}>{v}</p>)
            }
        </span>
        <Button disabled={isLoading} isLoading={isLoading} onClick={handleLogin}>Login</Button>
    </form>
}