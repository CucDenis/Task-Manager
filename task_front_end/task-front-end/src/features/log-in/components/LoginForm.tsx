import { useState } from "react";
import { Button } from "../../../shared/components/Button";
import { Input } from "../../../shared/components/Input";
import { PasswordInput } from "../../../shared/components/PasswordInput";
import { useLoginFormContext } from "../hooks/useLoginFormContext";
import { LoginFormProvider } from "../context/LoginForm/LoginFormProvider";
import { Switch } from "../../../shared/components/Switch";

const loginTypeOptions = [
  { value: "client", label: "Client" },
  { value: "technician", label: "Technician" },
];

export const LoginFormInner = () => {
  const { isLoading, errors, handleSubmit } = useLoginFormContext();
  const [loginType, setLoginType] = useState("client");
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await handleSubmit({ ...formData, userType: loginType });
  };

  return (
    <div className="container">
      <div className="row justify-content-center">
        <div className="col-md-6 col-lg-4">
          <div className="card mt-5">
            <div className="card-body">
              <h2 className="text-center mb-4">Login</h2>
              <form onSubmit={onSubmit}>
                <Switch
                  options={loginTypeOptions}
                  value={loginType}
                  onChange={setLoginType}
                  className="mb-4"
                />
                <Input
                  label="Email"
                  type="email"
                  value={formData.email}
                  error={errors.email}
                  onChange={(e) =>
                    setFormData({ ...formData, email: e.target.value })
                  }
                />

                <PasswordInput
                  label="Password"
                  value={formData.password}
                  error={errors.password}
                  onChange={(e) =>
                    setFormData({ ...formData, password: e.target.value })
                  }
                />

                <Button type="submit" isLoading={isLoading} fullWidth>
                  Login
                </Button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export const LoginForm = () => {
  return (
    <LoginFormProvider>
      <LoginFormInner />
    </LoginFormProvider>
  );
};
