use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day04::solve_part_one(input),
        Part::P2 => Day04::solve_part_two(input),
    }
}

const DIRECTIONS: [(isize, isize); 8] = [
    (1, 0),
    (0, 1),
    (1, 1),
    (1, -1),
    (-1, 0),
    (0, -1),
    (-1, -1),
    (-1, 1),
];

struct Day04;
impl Solution for Day04 {
    fn solve_part_one(input: String) -> String {
        let grid: Vec<Vec<char>> = input.lines().map(|line| line.chars().collect()).collect();

        let mut count = 0;
        for x in 0..grid.len() {
            for y in 0..grid[0].len() {
                for &(dx, dy) in &DIRECTIONS {
                    if check_direction(&grid, x as isize, y as isize, dx, dy) {
                        count += 1;
                    }
                }
            }
        }

        count.to_string()
    }

    fn solve_part_two(input: String) -> String {
        let grid: Vec<Vec<char>> = input.lines().map(|line| line.chars().collect()).collect();

        let mut count = 0;
        for x in 1..grid.len() - 1 {
            for y in 1..grid[0].len() - 1 {
                if grid[x][y] == 'A'
                    && ((grid[x - 1][y - 1] == 'M' && grid[x + 1][y + 1] == 'S')
                        || (grid[x - 1][y - 1] == 'S' && grid[x + 1][y + 1] == 'M'))
                    && ((grid[x - 1][y + 1] == 'M') && (grid[x + 1][y - 1] == 'S')
                        || (grid[x - 1][y + 1] == 'S') && (grid[x + 1][y - 1] == 'M'))
                {
                    count += 1;
                }
            }
        }

        count.to_string()
    }
}

fn check_direction(grid: &Vec<Vec<char>>, x: isize, y: isize, dx: isize, dy: isize) -> bool {
    for (i, &ch) in ['X', 'M', 'A', 'S'].iter().enumerate() {
        let nx = x + i as isize * dx;
        let ny = y + i as isize * dy;
        if nx < 0 || ny < 0 || nx >= grid.len() as isize || ny >= grid[0].len() as isize {
            return false;
        }
        if grid[nx as usize][ny as usize] != ch {
            return false;
        }
    }

    true
}
