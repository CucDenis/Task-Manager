type DateFieldProps = {
  value: string;
  onChange: (value: string) => void;
  label?: string;
  error?: string;
  className?: string;
};

export const DateField = ({
  value,
  onChange,
  label,
  error,
  className = "",
}: DateFieldProps) => {
  return (
    <div className="form-group">
      {label && <label className="form-label">{label}</label>}
      <input
        type="date"
        className={`form-control ${className}`}
        value={value}
        onChange={(e) => onChange(e.target.value)}
      />
      {error && <div className="text-danger small">{error}</div>}
    </div>
  );
};
