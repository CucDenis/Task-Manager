import { useState } from "react";
import { Input } from "./Input";
import { InputHTMLAttributes } from "react";

interface PasswordInputProps
  extends Omit<InputHTMLAttributes<HTMLInputElement>, "type"> {
  label: string;
  error?: string;
}

export const PasswordInput = (props: PasswordInputProps) => {
  const [showPassword, setShowPassword] = useState(false);

  return (
    <div className="position-relative">
      <Input {...props} type={showPassword ? "text" : "password"} />
      <button
        type="button"
        className="btn btn-link position-absolute end-0 top-50 translate-middle-y"
        onClick={() => setShowPassword(!showPassword)}
        style={{ marginTop: "10px" }}
      >
        {showPassword ? "Hide" : "Show"}
      </button>
    </div>
  );
};
