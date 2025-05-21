import axios, { AxiosInstance } from "axios";

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
            
            this.instance.interceptors.request.use((config)=>{
                const token = localStorage.getItem("jwt")
                if (token) {
                    config.headers.Authorization = `Bearer ${token}`;
                }
                return config;
            })
        }
        return this.instance
    }
}
