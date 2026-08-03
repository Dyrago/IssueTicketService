<script lang="ts">

</script>

<div class="centered-container">
<div class="card">
	<form method="POST">
	<div class="grid-item">
			<label>
				Email
				<input name="email" type="email">
			</label>
		</div>
		<div class="grid-item">
		<label>
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
		width: 900px;
		display: grid;
		padding: 1rem;
		margin-left: auto;
		margin-right: auto;
		flex: 1;
		justify-content: center;
		align-content: center;
	}

	.card
	{
		display: grid;
		grid-template-columns: repeat(12, 1fr);
		grid-template-rows: 1fr;
		flex-wrap: wrap;
		flex-grow: 1;
	}

	form{
		padding: 2rem;
		border: 1px solid black;
		border-radius: 20px;
		grid-column-start: 7;
		background-color: azure;
	}

	form > div > label
	{
		margin-top: 0.5rem;
		font-size: small;
	}

	form > div > label > input
	{
		border-radius: 5px;
		border-color: lightblue;
		margin-top: 0.5rem;
		height: 1.5rem;
	}

	form > button
	{
		background-color: #333;
		color: #fff;
		font-size: medium;
		border-radius: 10px;
		padding: 0.25rem;
		margin-top: 1rem;
	}

	.grid-item
	{
		grid: 1fr
	}

</style>