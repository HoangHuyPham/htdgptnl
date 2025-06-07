import { Navigate, Route, Routes } from "react-router"
import { SelfEvaluationPage, LoginPage, AccountPage, SelfEvaluationHistoryPage, ProfilePage } from "@pages"
import { HomeLayout } from "@layouts"
import { ValidationTokenLayout } from './../layouts/ValidationTokenLayout';
import { ValidationTokenPage } from './../pages/ValidationTokenPage';
import { UserProvider } from "@contexts/UserContext";

export const AppRoutes: React.FC = () => {
    return <Routes>
        {/* Public Route  */}

        <Route path="/" element={<Navigate to="/home" />} />

        <Route path="/home" element={<UserProvider><HomeLayout /></UserProvider>}>
            <Route index element={<AccountPage />} />
            <Route path="self-evaluation" element={<SelfEvaluationPage />} />
            <Route path="self-evaluation/history" element={<SelfEvaluationHistoryPage />} />
            <Route path="profile" element={<ProfilePage />} />
        </Route>


        <Route path="/validation-token/:token" element={<ValidationTokenLayout />}>
            <Route index element={<ValidationTokenPage />} />
        </Route>
        <Route path="/login" element={<LoginPage />} />


        {/* Private Route  */}
        <Route path="/admin">
        </Route>

        <Route path="*" element={<p className="text-center font-bold text-4xl">This page is not available 😢</p>} />
    </Routes>
}