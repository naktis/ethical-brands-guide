import '../Shared.css';

function SuccessMessage(props) {
  return(
    <span className="Success-message">{props.children}</span>
  )
}

export default SuccessMessage