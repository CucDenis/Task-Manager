import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useAppSelector } from "../../../app/redux/reduxHooks";
import { selectIsAuthenticated } from "../slice/authSlice";

export const ProtectedRoute = () => {
  const isAuthenticated = useAppSelector(selectIsAuthenticated);
  const location = useLocation();

  if (!isAuthenticated) {
    console.log("Not authenticated, redirecting to login");
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return <Outlet />;
};
