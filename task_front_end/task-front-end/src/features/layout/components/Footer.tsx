export const Footer = () => {
  const currentYear = new Date().getFullYear();

  return (
    <footer className="bg-light mt-auto py-3 border-top">
      <div className="container">
        <div className="row align-items-center">
          <div className="col-md-6 text-center text-md-start">
            <span className="text-muted">
              © {currentYear} Task Manager. All rights reserved.
            </span>
          </div>
          <div className="col-md-6 text-center text-md-end">
            <span className="text-muted">Version 1.0.0</span>
          </div>
        </div>
      </div>
    </footer>
  );
};
