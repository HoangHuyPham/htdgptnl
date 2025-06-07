import { Outlet } from "react-router"

export const ValidationTokenLayout: React.FC = ()=>{
    return <div className="w-full p-5">
        <Outlet />
    </div>
}