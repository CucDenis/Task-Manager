import { useContext } from "react";
import { LoginFormContext } from "../context/LoginForm/LoginFormContext";

export const useLoginFormContext = () => {
  const context = useContext(LoginFormContext);
  if (!context) {
    throw new Error(
      "useLoginFormContext must be used within LoginFormProvider"
    );
  }
  return context;
};
