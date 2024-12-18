use std::collections::{HashSet, VecDeque};

use crate::{
    core::{Part, Solution},
    utils::grid::Coordinate,
};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day18::solve_part_one(input),
        Part::P2 => Day18::solve_part_two(input),
    }
}

struct Day18;
impl Solution for Day18 {
    fn solve_part_one(input: String) -> String {
        let bytes: HashSet<Coordinate> = input
            .lines()
            .take(1024)
            .map(|b| {
                let (x, y) = b.split_once(',').unwrap();
                let x = x.parse().unwrap();
                let y = y.parse().unwrap();
                Coordinate { x, y }
            })
            .collect();

        bfs(
            Coordinate { x: 0, y: 0 },
            Coordinate { x: 70, y: 70 },
            bytes,
            71,
            71,
        )
        .unwrap()
        .to_string()
    }

    fn solve_part_two(input: String) -> String {
        let bytes: Vec<Coordinate> = input
            .lines()
            .map(|b| {
                let (x, y) = b.split_once(',').unwrap();
                let x = x.parse().unwrap();
                let y = y.parse().unwrap();
                Coordinate { x, y }
            })
            .collect();

        let start = Coordinate { x: 0, y: 0 };
        let end = Coordinate { x: 70, y: 70 };

        (1024..bytes.len())
            .find_map(|i| {
                if bfs(start, end, bytes.iter().cloned().take(i).collect(), 71, 71).is_none() {
                    let bad = bytes[i - 1];
                    println!("Found! {:?}", bad);
                    Some(format!("{},{}", bad.x, bad.y))
                } else {
                    None
                }
            })
            .unwrap_or("Unsolved".to_string())
    }
}

fn bfs(
    start: Coordinate,
    end: Coordinate,
    walls: HashSet<Coordinate>,
    width: isize,
    height: isize,
) -> Option<u32> {
    let mut queue: VecDeque<_> = [(start, 0)].into();
    let mut visit: HashSet<Coordinate> = [start].into();

    while let Some((position, cost)) = queue.pop_front() {
        if position == end {
            return Some(cost);
        }

        for neighbour in position.neighbours_orthogonal() {
            if neighbour != start
                && 0 <= neighbour.x
                && neighbour.x < width
                && 0 <= neighbour.y
                && neighbour.y < height
                && !walls.contains(&neighbour)
                && !visit.contains(&neighbour)
            {
                queue.push_back((neighbour, cost + 1));
                visit.insert(neighbour);
            }
        }
    }

    None
}
