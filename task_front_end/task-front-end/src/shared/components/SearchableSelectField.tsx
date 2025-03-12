import { useState, useRef, useEffect } from "react";

type SearchableOption = {
  id: number;
  firstName: string;
  lastName: string;
};

type SearchableSelectFieldProps = {
  value: string;
  onChange: (value: string) => void;
  options: SearchableOption[];
  placeholder?: string;
};

export const SearchableSelectField = ({
  value,
  onChange,
  options,
  placeholder,
}: SearchableSelectFieldProps) => {
  const [isOpen, setIsOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const wrapperRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (
        wrapperRef.current &&
        !wrapperRef.current.contains(event.target as Node)
      ) {
        setIsOpen(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  const getFullName = (option: SearchableOption) =>
    `${option.firstName} ${option.lastName}`;

  const filteredOptions = options.filter((option) =>
    getFullName(option).toLowerCase().includes(searchTerm.toLowerCase())
  );

  const selectedOption = options.find((opt) => opt.id.toString() === value);

  return (
    <div className="position-relative" ref={wrapperRef}>
      <input
        type="text"
        className="form-control"
        placeholder={placeholder}
        value={selectedOption ? getFullName(selectedOption) : searchTerm}
        onChange={(e) => {
          setSearchTerm(e.target.value);
          setIsOpen(true);
        }}
        onFocus={() => setIsOpen(true)}
      />

      {isOpen && filteredOptions.length > 0 && (
        <div className="position-absolute w-100 mt-1 shadow bg-white rounded-2 z-3">
          <ul className="list-unstyled mb-0 py-2 max-height-200 overflow-auto">
            {filteredOptions.map((option) => (
              <li
                key={option.id}
                className="px-3 py-2 cursor-pointer hover-bg-light"
                onClick={() => {
                  onChange(option.id.toString());
                  setSearchTerm("");
                  setIsOpen(false);
                }}
              >
                {getFullName(option)}
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
};
