<script lang="ts">

</script>

<div class="centered-container">
<div class="card">
	<form method="POST">
	<div class="grid-item">
			<label for="email">
				Email
				<br />
				<input name="email" type="email">
			</label>
		</div>
		<div class="grid-item">
		<label for="password" >
			Password
			<input name="password" type="password">
		</label>
		</div>
		<button>Log in</button>
	</form>
</div>
</div>

<style>
	.centered-container{
		width: 100%;
		display: grid;
		padding: 1rem;
		margin-left: auto;
		margin-right: auto;
		justify-content: center;
		align-content: center;

	}

	.card
	{
		display: grid;
		grid-template-rows: 1fr;
		padding: 2rem;
		border: 2px solid black;
		border-radius: 20px;
		background-color: #fff;
		
	}

	form > div > label
	{
		margin-top: 1rem;
		font-size: large;
		width: 100%;
		font-weight: 500;
	}

	form > div > label > input
	{
		border-radius: 5px;
		border-color: lightblue;
		margin-top: 0.75rem;
		border: 0.5px solid black;
		height: 2rem;
		width: 100%;
	}

	form > button
	{
		width: 100%;
		height: 3rem;
		background-color: #333;
		color: #fff;
		font-size: medium;
		border-radius: 10px;
		padding: 0.25rem;
		margin-top: 1rem;
		font-weight: bold;
	}

	form > button:hover
	{
		cursor: pointer;
		background-color: #474747;
	}


	.grid-item
	{
		margin-top: 1rem;
		grid: 1fr
	}

</style>