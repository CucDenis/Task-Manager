import { useState, useEffect } from "react";
import { Button } from "../../../shared/components/Button";
import { DateField } from "../../../shared/components/DateField";

type InterventionFileDetailsModalProps = {
  intervention: InterventionFile | null;
  onClose: () => void;
  onUpdate: (intervention: InterventionFile) => void;
};

export const InterventionFileDetailsModal = ({
  intervention,
  onClose,
  onUpdate,
}: InterventionFileDetailsModalProps) => {
  const [editMode, setEditMode] = useState<{ [key: string]: boolean }>({});
  const [editedData, setEditedData] = useState<InterventionFile | null>(null);
  const [isChanged, setIsChanged] = useState(false);

  useEffect(() => {
    setEditedData(intervention);
    setEditMode({});
    setIsChanged(false);
  }, [intervention]);

  if (!intervention || !editedData) return null;

  const handleEdit = (field: keyof InterventionFile) => {
    setEditMode({ ...editMode, [field]: true });
  };

  const handleChange = (field: keyof InterventionFile, value: string) => {
    setEditedData({ ...editedData, [field]: value });
    setIsChanged(true);
  };

  const handleUpdate = () => {
    if (editedData) {
      onUpdate(editedData);
      onClose();
    }
  };

  const renderField = (field: keyof InterventionFile, label: string) => {
    const value = editedData[field];
    return (
      <div className="mb-3">
        <label className="form-label">{label}</label>
        <div onClick={() => handleEdit(field)}>
          {editMode[field] ? (
            field === "interventionDate" ? (
              <DateField
                value={String(value)}
                onChange={(newValue) => handleChange(field, newValue)}
              />
            ) : (
              <input
                type="text"
                className="form-control"
                value={value}
                onChange={(e) => handleChange(field, e.target.value)}
              />
            )
          ) : (
            <div className="p-2 border rounded bg-light">{value}</div>
          )}
        </div>
      </div>
    );
  };

  return (
    <div className="modal show d-block" tabIndex={-1}>
      <div className="modal-dialog modal-lg">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Intervention Details</h5>
            <button
              type="button"
              className="btn-close"
              onClick={onClose}
            ></button>
          </div>
          <div className="modal-body">
            {renderField("interventionDate", "Date")}
            {renderField("technicianName", "Technician")}
            {renderField("clientName", "Customer")}
            {renderField("workPointAddress", "Work Point Address")}
            {renderField("contactPerson", "Contact Person")}
            {renderField("timeInterval", "Time Interval")}
            {renderField("status", "Status")}
          </div>
          <div className="modal-footer">
            {isChanged && (
              <Button onClick={handleUpdate}>Update Intervention</Button>
            )}
            <Button variant="outline" onClick={onClose}>
              Close
            </Button>
          </div>
        </div>
      </div>
      <div className="show"></div>
    </div>
  );
};
