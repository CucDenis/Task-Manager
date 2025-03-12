type SwitchOption = {
  value: string;
  label: string;
};

type SwitchProps = {
  options: SwitchOption[];
  value: string;
  onChange: (value: string) => void;
  className?: string;
};

export const Switch = ({
  options,
  value,
  onChange,
  className = "",
}: SwitchProps) => {
  return (
    <div className={`btn-group w-100 ${className}`}>
      {options.map((option) => (
        <button
          key={option.value}
          type="button"
          className={`btn ${
            value === option.value ? "btn-primary" : "btn-outline-primary"
          }`}
          onClick={() => onChange(option.value)}
        >
          {option.label}
        </button>
      ))}
    </div>
  );
};
