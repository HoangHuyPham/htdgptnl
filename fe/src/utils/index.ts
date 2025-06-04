import { jwtDecode } from 'jwt-decode'
import moment from 'moment';

export const validToken = (token: string)=>{
    const tokenDecoded = jwtDecode<{exp: number}>(token)
    return (moment().unix() - tokenDecoded?.exp) < 0
}

export const decodeToken = (token: string)=>{
    return jwtDecode<{exp:number, sub:string}>(token)
}