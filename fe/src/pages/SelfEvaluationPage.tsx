import { FormTable } from "@components/FormTable"
import { IEvaluationSchedule } from "@interfaces"
import { AppRequest } from "@requests"
import { AxiosError } from "axios"
import { useEffect, useState } from "react"
import { toast } from "react-toastify"

export const SelfEvaluationPage: React.FC = () => {
    const [evaluationSchedules, setevaluationSchedules] = useState<IEvaluationSchedule[]>([])

    useEffect(() => {
        fetchEvaluationForms()
    }, [])

    const fetchEvaluationForms = async () => {
        try {
            const resp = await AppRequest.getInstance().get("api/EvaluationSchedule/self")
            const results: IEvaluationSchedule[] = resp?.data?.data
            setevaluationSchedules([...results])
        } catch (e) {
            const err = e as AxiosError
            toast.error(`Has error (${err?.code})`)
        }
    }

    return <>
        {
            (evaluationSchedules.length > 0) && evaluationSchedules.map((v, i) => <FormTable key={i} evaluationSchedule={v} />) || <p className="text-4xl font-semibold text-center">No evaluation form now ✔</p>
        }
    </>
}