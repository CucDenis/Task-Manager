type Option = string | { id: number; firstName: string; lastName: string };

type SelectFieldProps = {
  value: string;
  onChange: (value: string) => void;
  options: Option[];
  placeholder?: string;
};

export const SelectField = ({
  value,
  onChange,
  options,
  placeholder,
}: SelectFieldProps) => {
  console.log("options: ", options);

  const getOptionLabel = (option: Option): string => {
    if (typeof option === "string") return option;
    return `${option.firstName} ${option.lastName}`;
  };

  const getOptionValue = (option: Option): string => {
    if (typeof option === "string") return option;
    return option.id.toString();
  };

  return (
    <select
      className="form-select"
      value={value}
      onChange={(e) => onChange(e.target.value)}
    >
      <option value="">{placeholder}</option>
      {options.map((option) => (
        <option key={getOptionValue(option)} value={getOptionValue(option)}>
          {getOptionLabel(option)}
        </option>
      ))}
    </select>
  );
};
