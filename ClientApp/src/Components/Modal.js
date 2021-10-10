// Based on srouce code found here: https://www.digitalocean.com/community/tutorials/react-modal-component

const Modal = ({ handleClose, show, children, title }) => {
    const showHideClassName = show ? "modal display-block" : "modal display-none";
  
    return (
      <div className={showHideClassName}>
        <section className="modal-main">
          <div className="Modal-header">
						<h3>{title}</h3>
            <button type="button" onClick={handleClose}>
              X
            </button>
					</div>
          {children}
        </section>
      </div>
    );
};

export default Modal;