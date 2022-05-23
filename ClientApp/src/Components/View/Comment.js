function Comment({ comment }) {
  return(
    <li className="Comment-row">
      {comment.text}
    </li>
  )
}

export default Comment