import { Navigate, Route, Routes } from "react-router"
import { SelfEvaluationPage, LoginPage, AccountPage, SelfEvaluationHistoryPage } from "@pages"
import { HomeLayout } from "@layouts"

export const AppRoutes: React.FC = () => {
    return <Routes>
        {/* Public Route  */}

        <Route path="/" element={<Navigate to="/home" />} />

        <Route path="/home" element={<HomeLayout />}>
            <Route index element={<AccountPage />} />
            <Route path="self-evaluation" element={<SelfEvaluationPage />} />
            <Route path="self-evaluation/history" element={<SelfEvaluationHistoryPage />} />
        </Route>
        <Route path="/login" element={<LoginPage />} />


        {/* Private Route  */}
        <Route path="/admin">
        </Route>

        <Route path="*" element={<p className="text-center font-bold text-4xl">This page is not available 😢</p>} />
    </Routes>
}