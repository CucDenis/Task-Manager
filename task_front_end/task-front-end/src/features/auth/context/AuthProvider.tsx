import { FC, ReactNode, useEffect, useState } from "react";
import { AuthContext } from "./AuthContext";
import { useAppDispatch, useAppSelector } from "../../../app/redux/reduxHooks";
import { checkAuthStatus, selectIsAuthenticated } from "../slice/authSlice";
import { logoutThunk } from "../slice/userSlice";

export const AuthProvider: FC<{ children: ReactNode }> = ({ children }) => {
  const dispatch = useAppDispatch();
  const isAuthenticated = useAppSelector(selectIsAuthenticated);
  const [hasChecked, setHasChecked] = useState(false);

  const checkAuth = async () => {
    try {
      await dispatch(checkAuthStatus()).unwrap();
    } catch (error) {
      console.error("Auth check failed:", error);
    } finally {
      setHasChecked(true);
    }
  };

  useEffect(() => {
    checkAuth();
  }, [dispatch]);

  const logout = async () => {
    await dispatch(logoutThunk());
  };

  // Don't render children until we've checked auth
  if (!hasChecked) {
    return (
      <div className="d-flex justify-content-center align-items-center min-vh-100">
        <div className="spinner-border" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
      </div>
    );
  }

  return (
    <AuthContext.Provider value={{ isAuthenticated, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
