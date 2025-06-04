import axios, { AxiosError, AxiosInstance } from "axios";
import { toast } from "react-toastify";

export class AppRequest {
    static instance: AxiosInstance

    static getInstance = (): AxiosInstance => {
        if (!this.instance) {
            this.instance = axios.create({
                baseURL: "https://localhost:7061/",
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("jwt")}`,
                }
            })

            this.instance.interceptors.request.use((config) => {
                const token = localStorage.getItem("jwt")
                if (token) {
                    config.headers.Authorization = `Bearer ${token}`;
                }
                return config;
            })

            this.instance.interceptors.response.use(
                (resp) => resp,
                (e) => {
                    const err = e as AxiosError
                    
                    if (err.status === 401) {
                        window.location.href = "/login"
                    }else if (err.status === 403) {
                        // do nothing
                    }else{
                        if (err.request)
                            toast.error("Check your network")
                    }
                    return Promise.reject(e)
                }
            )
        }
        return this.instance
    }
}
