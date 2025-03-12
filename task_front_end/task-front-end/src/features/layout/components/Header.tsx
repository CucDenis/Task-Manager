import { useEffect, useRef } from "react";
import { Link } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../../app/redux/reduxHooks";
import { selectIsAuthenticated } from "../../auth/slice/authSlice";
import { ProfileDropdown } from "../../../shared/components/ProfileDropdown";
import { selectUser, logoutThunk } from "../../auth/slice/userSlice";
import Collapse from "bootstrap/js/dist/collapse";

export const Header = () => {
  const dispatch = useAppDispatch();
  const isAuthenticated = useAppSelector(selectIsAuthenticated);
  const user = useAppSelector(selectUser);
  const collapseRef = useRef<Collapse | null>(null);
  const navbarRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    // Initialize collapse on mount
    if (navbarRef.current) {
      collapseRef.current = new Collapse(navbarRef.current, {
        toggle: false,
      });
    }

    // Cleanup on unmount
    return () => {
      collapseRef.current?.dispose();
    };
  }, []);

  const handleToggle = () => {
    collapseRef.current?.toggle();
  };

  const handleLogout = () => {
    dispatch(logoutThunk());
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
      <div className="container">
        <Link to="/" className="navbar-brand">
          Task Manager
        </Link>

        <button
          className="navbar-toggler"
          type="button"
          onClick={handleToggle}
          aria-controls="navbarContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        <div
          className="collapse navbar-collapse"
          id="navbarContent"
          ref={navbarRef}
        >
          <div className="ms-auto">
            <ProfileDropdown
              isAuthenticated={isAuthenticated}
              userName={user.fullName}
              email={user.email}
              onLogout={handleLogout}
            />
          </div>
        </div>
      </div>
    </nav>
  );
};
