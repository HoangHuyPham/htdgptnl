import { Route, Routes } from "react-router"
import { HomePage, LoginPage } from "@pages"

export const AppRoutes: React.FC = () => {
    return <>
        <Routes>
            {/* Public Route  */}
            <Route>
                <Route index element={<HomePage/>} />
                <Route path="/home" element={<HomePage/>} />
                <Route path="/login" element={<LoginPage/>} />
            </Route>

            {/* Private Route  */}
            <Route path="/admin">
            </Route>

            <Route path="*" element={<p className="text-center font-bold text-4xl">This page is not available :(</p>}/>
        </Routes>
    </>
}