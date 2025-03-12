declare type LoginFormData = {
  email: string;
  password: string;
  userType: string;
};

declare type LoginFormContextData = {
  isLoading: boolean;
  errors: Partial<LoginFormData>;
  handleSubmit: (formData: LoginFormData) => Promise<void>;
};
