import { Link } from 'react-router-dom'

const Modal = ({ handleClose, show, children, title, editable, editBrand, deleteBrand }) => {
    const showHideClassName = show ? "modal display-block" : "modal display-none";

    const exitToEdit = () => {
      handleClose();
      editBrand();
    }

    const exitToDelete = () => {
      handleClose();
      deleteBrand();
    }

    const makeBrandButtons = () => {
      if (editable === true) {
        return (
          <div className="Brand-buttons">
            <button type="button" onClick={exitToEdit}>&#9998;</button>
            <button type="button" onClick={exitToDelete}>&#128465;</button>
          </div>
        )
      }
    }

    const makeTitle = () => {
      if (title !== "")
        return <h3>{title}</h3>
    }
  
    return (
      <div className={showHideClassName}>
        <section className="modal-main">
          <div className="Modal-header">
						{ makeTitle() }
            { makeBrandButtons() }

            <Link to="/">
              <button type="button" onClick={handleClose}>X</button>
					  </Link>

					</div>
          {children}
        </section>
      </div>
    );
};

export default Modal;