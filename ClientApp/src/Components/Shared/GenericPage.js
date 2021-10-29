import BackButton from './BackButton';
import './Shared.css';

function GenericPage(props) {
  return(
    <div className="Generic-page">
      <div><BackButton /></div>
      <div className="Generic-content-div">
        <div className="Generic-content">
          {props.children}
        </div>
      </div>
      <div></div>
    </div>
  )
}

export default GenericPage;