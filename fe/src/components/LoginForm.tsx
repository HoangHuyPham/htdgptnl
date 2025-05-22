import { Button } from "@components"
import { AppRequest } from "@requests"
import { AxiosError } from "axios"
import { MouseEvent, useState } from "react"
import { toast } from "react-toastify"

export const LoginForm: React.FC = () => {
    const [isLoading, setLoading] = useState(false)
    const [username, setUsername] = useState<string>("")
    const [password, setPassword] = useState<string>("")

    const handleLogin = async (e: MouseEvent) => {
        e.preventDefault()
        setLoading(true)
        try { 
            const resp = await AppRequest.getInstance().post("api/Auth/login", { username, password })
            if (resp.data?.message === "login success") {
                localStorage.setItem("jwt", resp.data?.data)
                toast.success(resp.data?.message)
            }else
                toast.error(resp.data?.message)
        } catch (err) {
            const e = err as AxiosError
            toast.error(e?.message)
            console.error(err)
        } finally {
            setLoading(false)
        }

    }

    return <form className="flex box-border flex-col animate-fadeIn bg-white text-simple-black p-10 gap-4 rounded-xl">
        <label className="text-center text-2xl font-semibold">Login to account</label>
        <span className="flex flex-col gap-5 font-semibold">
            <input className="bg-amber-200 w-[240px]" type="text" value={username} onChange={(e) => setUsername(e.target.value)} placeholder="username / email" />
            <input className="bg-amber-200 w-[240px]" type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="password" />
        </span>
        <Button isLoading={isLoading} onClick={handleLogin}>Login</Button>
    </form>
}