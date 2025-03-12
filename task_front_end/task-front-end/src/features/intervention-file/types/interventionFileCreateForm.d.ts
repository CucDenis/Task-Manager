declare type InterventionFileCreateFormContextData = {
  isLoading: boolean;
  errors: Partial<InterventionFileFormData>;
  handleSubmit: (formData: InterventionFileFormData) => Promise<void>;
};

declare type InterventionFileFormData = {
  date: string;
  technician: string;
  customer: string;
  description: string;
  status: string;
  systemType: string;
};
