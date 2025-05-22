import { jwtDecode } from 'jwt-decode'
import moment from 'moment';

export const validToken = (token: string)=>{
    const tokenDecoded = jwtDecode<{exp: number}>(token)
    console.log(moment.unix(tokenDecoded?.exp).fromNow())
    return (moment.now() - tokenDecoded?.exp) > 0
}