import '../Shared.css';

function ValidationError(props) {
  return(
    <span className="Validation-error">{props.children}</span>
  )
}

export default ValidationError;