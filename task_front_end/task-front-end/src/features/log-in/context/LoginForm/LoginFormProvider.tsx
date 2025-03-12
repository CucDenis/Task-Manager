import { FC, ReactNode, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  selectAuthIsLoading,
  checkAuthStatus,
} from "../../../auth/slice/authSlice";
import { LoginFormContext } from "./LoginFormContext";
import {
  useAppDispatch,
  useAppSelector,
} from "../../../../app/redux/reduxHooks";
import { loginThunk } from "../../../auth/slice/userSlice";

export const LoginFormProvider: FC<{ children: ReactNode }> = ({
  children,
}) => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const isLoading = useAppSelector(selectAuthIsLoading);
  const [errors, setErrors] = useState<Partial<LoginFormData>>({});

  const validateForm = (formData: LoginFormData) => {
    const newErrors: Partial<LoginFormData> = {};

    if (!formData.email) newErrors.email = "Email is required";
    if (!formData.password) newErrors.password = "Password is required";
    if (formData.email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      newErrors.email = "Invalid email format";
    }
    setErrors(newErrors);

    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (formData: LoginFormData) => {
    if (!validateForm(formData)) return;

    try {
      await dispatch(loginThunk(formData)).unwrap();
      await dispatch(checkAuthStatus()).unwrap();
      navigate("/");
    } catch (error: unknown) {
      console.error("Failed to login:", error);
      setErrors({
        password:
          error instanceof Error ? error.message : "Invalid credentials",
      });
    }
  };

  return (
    <LoginFormContext.Provider
      value={{
        isLoading,
        errors,
        handleSubmit,
      }}
    >
      {children}
    </LoginFormContext.Provider>
  );
};
