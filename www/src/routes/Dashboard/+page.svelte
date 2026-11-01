<div id="task-grid">
    <div class="dashboard-filters">
        <input type="text" placeholder="Title"/>
        <input type="text" placeholder="Content"/>
        <input type="datetime" />
        <input type="datetime" />
        <select>
            <option>New</option>
            <option>In Progress</option>
            <option>Done</option>
        </select>
        <select>
            <option>admin@tickets.com</option>
            <option>mod@tickets.com</option>
            <option>user@tickets.com</option>
        </select>
        <button>Search</button>
    </div>
    <nav class="dashboard-actions">
        <button id="btn-create"> Create</button>
        <button id="btn-create"> Create</button>
        <button id="btn-create"> Create</button>
        <button id="btn-create"> Create</button>
    </nav>
    <div class="task-col">
        <div class="col-label">New</div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
    </div>
    <div class="task-col">
        <div class="col-label">In Progress</div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
    </div>
    <div class="task-col">
        <div class="col-label">Done</div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
        <div class="task-item">
            <h4>This is task item title</h4>
        </div>
    </div>
</div>

<style>
    #task-grid{
        width: 100%;
        display: grid;
        grid-template-columns: repeat(12, 1fr);
    }

    .dashboard-actions
    {
        grid-column: span 12;
        padding: 1rem;
    }

    .dashboard-filters
    {
        padding: 0.5rem;
        border-bottom: 1px solid #000;
        grid-column: span 12;
    }

    .dashboard-actions > button
    {
        font-weight: 600;
        font-size: medium;
        padding: 0.5rem;
        border: 0.5px solid #000;
        border-radius: 20px;
        margin-right: 0.5rem;
        cursor: pointer;
    }

    #btn-create{
        background-color: rgb(28, 190, 28);
        color: #fff;
    }

    .col-label{
        background-color: #c7c7c7;
        font-size: medium;
        font-weight: 600;
        text-align: center;
        padding: 0.5rem;
        border: 0.5px solid #000;
    }

    .task-col
    {
        grid-column: span 4;
        border-right: 1px solid #000;
        max-height: 100vh;
        overflow-y: scroll;
    }

    .task-item
    {
        border: 1px solid #000;
        border-radius: 20px;
        padding: 0.5rem;
        margin: 0.5rem 0.5rem 0.5rem 0.5rem;
    }

</style>