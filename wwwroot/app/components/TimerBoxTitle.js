import React from 'react';

const TimerBoxTitle = ({onTimerBoxTitleSet, eachTitle }) => {
	let titleSetInput; 
	return (
		<div className="TimerBoxTitle-container">
			<input className="TimerBoxTitle-input mdl-textfield__input" size='10' type="text" placeholder="Title"
				value={eachTitle || ''}
				ref={node => {let titleSetInput = node;}}
				onChange={() => {onTimerBoxTitleSet(titleSetInput); }} 
			/>
			</div>
		);
	};

	export default TimerBoxTitle;
