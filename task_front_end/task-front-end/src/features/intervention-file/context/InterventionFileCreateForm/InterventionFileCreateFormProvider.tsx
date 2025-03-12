import { FC, ReactNode, useState } from "react";
import { useNavigate } from "react-router-dom";
import { InterventionFileCreateFormContext } from "./InterventionFileCreateFormContext";

export const InterventionFileCreateFormProvider: FC<{
  children: ReactNode;
}> = ({ children }) => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);
  const [errors, setErrors] = useState<Partial<InterventionFileFormData>>({});

  const validateForm = (formData: InterventionFileFormData) => {
    const newErrors: Partial<InterventionFileFormData> = {};
    if (!formData.date) newErrors.date = "Date is required";
    if (!formData.technician) newErrors.technician = "Technician is required";
    if (!formData.customer) newErrors.customer = "Customer is required";
    if (!formData.description)
      newErrors.description = "Description is required";
    if (!formData.status) newErrors.status = "Status is required";
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (formData: InterventionFileFormData) => {
    if (!validateForm(formData)) return;

    setIsLoading(true);
    try {
      // Add your API call here
      await new Promise((resolve) => setTimeout(resolve, 1000));
      navigate("/");
    } catch (error) {
      console.error("Failed to create intervention:", error);
    } finally {
      setIsLoading(false);
    }
  };

  const contextValue = {
    isLoading,
    errors,
    handleSubmit,
  };

  return (
    <InterventionFileCreateFormContext.Provider value={contextValue}>
      {children}
    </InterventionFileCreateFormContext.Provider>
  );
};
