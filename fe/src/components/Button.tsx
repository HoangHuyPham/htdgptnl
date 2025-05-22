import { ButtonHTMLAttributes, ReactNode } from "react"
import loadingSpinner from "/loading.svg"

export type ButtonProps = {
    children?: ReactNode,
    isLoading?: boolean,
    props?: ButtonHTMLAttributes<HTMLButtonElement>
}

export const Button : React.FC<ButtonHTMLAttributes<HTMLButtonElement> & {isLoading?: boolean}> = ({children, isLoading, ...props})=>{
    return (<button className="primary flex justify-center" {...props}>
        {isLoading && <img className="w-[25px] h-[25px]" src={loadingSpinner}/>}
        <span className="text-simple-white">{children}</span>
    </button>)
}