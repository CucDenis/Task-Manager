import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "../../../shared/components/Button";
import { TextArea } from "../../../shared/components/TextArea";
import { SelectField } from "../../../shared/components/SelectField";
import { InterventionFileCreateFormProvider } from "../context/InterventionFileCreateForm/InterventionFileCreateFormProvider";
import { useInterventionFileCreateContext } from "../hooks/useInterventionFileCreateContext";

export const InterventionFileCreateFormInner = () => {
  const navigate = useNavigate();
  const { isLoading, errors, handleSubmit } =
    useInterventionFileCreateContext();
  const [formData, setFormData] = useState<InterventionFileFormData>({
    date: new Date().toISOString().split("T")[0],
    technician: "",
    customer: "",
    description: "",
    status: "Scheduled",
    systemType: "",
  });

  const systemTypes = [
    "HVAC",
    "Electrical",
    "Plumbing",
    "Security",
    "Fire System",
    "Network",
    "Elevator",
    "Generator",
  ];

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await handleSubmit(formData);
  };

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Create New Intervention</h2>
        <Button variant="outline" onClick={() => navigate("/")}>
          Cancel
        </Button>
      </div>

      <form onSubmit={onSubmit} className="row g-3">
        <div className="col-md-6">
          <SelectField
            value={formData.systemType}
            onChange={(value) =>
              setFormData({ ...formData, systemType: value })
            }
            options={systemTypes}
            placeholder="Select System Type"
          />
        </div>
        <div className="col-12">
          <TextArea
            label="Description"
            rows={4}
            value={formData.description}
            onChange={(e) =>
              setFormData({ ...formData, description: e.target.value })
            }
            error={errors.description}
          />
        </div>
        <div className="col-12 mt-4">
          <Button type="submit" isLoading={isLoading} fullWidth>
            Create Intervention
          </Button>
        </div>
      </form>
    </div>
  );
};

export const InterventionFileCreateForm = () => {
  return (
    <InterventionFileCreateFormProvider>
      <InterventionFileCreateFormInner />
    </InterventionFileCreateFormProvider>
  );
};
