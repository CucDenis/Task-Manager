import { useState, useRef, useEffect } from "react";
import { Button } from "./Button";

type ProfileDropdownProps = {
  isAuthenticated: boolean;
  userName?: string;
  email?: string;
  onLogout: () => void;
};

export const ProfileDropdown = ({
  isAuthenticated,
  userName = "",
  email = "",
  onLogout,
}: ProfileDropdownProps) => {
  const [isOpen, setIsOpen] = useState(false);
  const dropdownRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (
        dropdownRef.current &&
        !dropdownRef.current.contains(event.target as Node)
      ) {
        setIsOpen(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  const handleLogout = () => {
    setIsOpen(false);
    onLogout();
  };

  if (!isAuthenticated) {
    return (
      <div className="nav-item">
        <span className="nav-link text-muted">No User Logged</span>
      </div>
    );
  }

  return (
    <div className="dropdown" ref={dropdownRef}>
      <button
        className="btn btn-link nav-link dropdown-toggle"
        onClick={() => setIsOpen(!isOpen)}
        type="button"
      >
        {userName}
      </button>
      <ul className={`dropdown-menu dropdown-menu-end ${isOpen ? "show" : ""}`}>
        <li className="dropdown-item-text">
          <div className="fw-bold">{userName}</div>
          <div className="small text-muted">{email}</div>
        </li>
        <li>
          <hr className="dropdown-divider" />
        </li>
        <li>
          <Button
            variant="outline"
            className="dropdown-item text-danger"
            onClick={handleLogout}
          >
            Logout
          </Button>
        </li>
      </ul>
    </div>
  );
};
