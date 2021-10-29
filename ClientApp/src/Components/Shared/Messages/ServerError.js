import '../Shared.css';

function ServerError(props) {
  return(
    <span className="Server-error">{props.children}</span>
  )
}

export default ServerError