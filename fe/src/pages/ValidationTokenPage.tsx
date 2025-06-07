import { AppRequest } from "@requests"
import { AxiosError, HttpStatusCode } from "axios"
import { useEffect, useState } from "react"
import { useParams } from "react-router"
import { toast } from "react-toastify"
import loadingSpinner from "/loading_black.svg"

export const ValidationTokenPage: React.FC = () => {
    const params = useParams()
    const [stateToken, setStateToken] = useState<"pending" | "valid" | "invalid">("pending")
    const [message, setMessage] = useState("")

    useEffect(() => {
        validateToken()
    }, [])

    const validateToken = async () => {
        const token = params.token
        try {
            const resp = await AppRequest.getInstance().get("api/ValidationToken/ChangeEmail", {
                params: {
                    token
                }
            })

            if (resp.status === HttpStatusCode.NoContent) {
                setMessage(`Ok`)
                setStateToken(`valid`)
            } else if (resp.status === HttpStatusCode.Ok) {
                setMessage(`${resp.data?.message}`)
                setStateToken('invalid')
            }

        } catch (e) {
            const err = e as AxiosError
            setMessage(`Token is invalid`)
            setStateToken(`invalid`)
            console.log(`Has error (${err?.code})`)
        }
    }
    return <div className=" bg-simple-white w-[30%] flex flex-col mx-auto rounded-xl shadow-xl">
        <span className="text-center text-2xl font-semibold text-simple-black">Validation token</span>
        {
            stateToken === 'pending' && (<span className="text-blue-500 text-center flex mx-auto">Validating <img className="w-[25px] h-[25px]" src={loadingSpinner} /></span>)
        }
        {
            stateToken === 'valid' && (<>
                <span className="text-green-500 text-center flex mx-auto">
                    Validate successful ✔
                </span>
                <span className="text-center">
                    Message: {message}
                </span>
            </>)
        }
        {
            stateToken === 'invalid' && (<>
                <span className="text-red-500 text-center flex mx-auto">
                    Validate failed ❌
                </span>
                <span className="text-center">
                    Message: {message}
                </span>
            </>)
        }
    </div>
}