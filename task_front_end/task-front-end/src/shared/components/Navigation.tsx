import { Link } from "react-router-dom";

type NavigationProps = {
  items: NavigationItem[];
};

export const Navigation = ({ items }: NavigationProps) => {
  return (
    <nav className="navbar-nav me-auto mb-2 mb-lg-0">
      {items.map((item) => (
        <li key={item.path} className="nav-item">
          <Link to={item.path} className="nav-link">
            {item.label}
          </Link>
        </li>
      ))}
    </nav>
  );
};
