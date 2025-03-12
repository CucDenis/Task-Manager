import { TextareaHTMLAttributes } from "react";

interface TextAreaProps extends TextareaHTMLAttributes<HTMLTextAreaElement> {
  label?: string;
  error?: string;
}

export const TextArea = ({
  label,
  error,
  className = "",
  ...props
}: TextAreaProps) => {
  return (
    <div className="form-group">
      {label && <label className="form-label">{label}</label>}
      <textarea className={`form-control ${className}`} {...props} />
      {error && <div className="text-danger small">{error}</div>}
    </div>
  );
};
