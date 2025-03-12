import { createContext } from "react";

export const AuthContext = createContext<AuthStatusContextData>(
  {} as AuthStatusContextData
);
