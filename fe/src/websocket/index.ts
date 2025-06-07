import { ISocketMessage } from "@interfaces"
import { toast } from "react-toastify"
import { Dispatch } from 'react';
import { USER_ACTION, UserAction } from './../contexts/UserContext';

export class AppSocket {
    static instance: AppSocket
    socket: WebSocket | undefined

    static getInstance() {
        if (!this.instance) {
            this.instance = new AppSocket()
        }
        return this.instance
    }

    init(url: string, dispatch: Dispatch<UserAction>) {
        this.socket = new WebSocket(url)
        this.socket.onopen = () => {
            console.log("socket connected")
        }

        this.socket.onclose = (e) => {
            console.log(`socket closed: ${e}`)
        }

        this.socket.onerror = (error) => {
            console.error("socket error:", error);
        };

        this.socket.onmessage = (message) => {
            try {
                const data: ISocketMessage = JSON.parse(message.data)
                if (data.type === "changeEmail") {
                    toast.info("Email has changed")
                    dispatch({
                        type: USER_ACTION.UPDATE,
                        payload: {
                            email: data.content
                        }
                    })
                }else if (data.type === "notify"){
                    console.log(data.content)
                }
            } catch (e) {
                console.error(e)
            }
        };
    }
}
